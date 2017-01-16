using System;
using System.Collections.Generic;
using System.Linq;
using XDI.Code;
using XDI.Code.Outputs;
using XDI.Core.DependecyInjection;
using XDI.Core.DependencyInjection;
using XDI.Core.Outputs;
using XDI.CSharp.ComponentModel;
using XDI.Generators.CSharp;
using XDI.Generators.CSharp.CodeGeneration;
using XDI.Generators.CSharp.CodeGeneration.Dataflip;
using XDI.Providers.JsonConfiguration;
using XDI.Providers.SprocTesting.Builder;
using XDI.Providers.SprocTesting.SettingsReaders;

namespace XDI.Providers
{
    public class CSharpDataflipFromJson
    {
        private readonly CodeOutput _output = null;
        private readonly IJsonConfigurationProvider _jsonProvider = null;
        private IEnumerable<IChainedContextCodeGenerator> _chains = null;
        private List<ICodeGenerator> _chainedGenerators = new List<ICodeGenerator>();

        public CSharpDataflipFromJson(CodeOutput output, IJsonConfigurationProvider jsonProvider)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));
            if (jsonProvider == null)
                throw new ArgumentNullException(nameof(jsonProvider));

            _jsonProvider = jsonProvider;
            _output = output;
        }

        public CSharpDataflipFromJson(string jsonConfigurationFile = "dataflip.json")
            :
            this(
                new CodeOutput(
                    new ICodeWriter[] { new ConsoleOutput(), new DirectoryCodeOutput() }
                ),
                new FromFileJsonConfigurationProvider(jsonConfigurationFile)
            )
        {}

        public CSharpDataflipFromJson(ICodeWriter codeOutput, string jsonConfigurationFile = "dataflip.json")
            :
            this(
                new CodeOutput (new ICodeWriter[] { codeOutput } )
                ,
                new FromFileJsonConfigurationProvider(jsonConfigurationFile)
            )
        { }

        public CSharpDataflipFromJson Chain(IEnumerable<IChainedContextCodeGenerator> chains)
        {
            if (chains == null)
                throw new ArgumentNullException(nameof(chains));

            _chains = chains;
            return this;
        }

        private CodeContext ChainGeneratorToCodeContext(CodeContext context)
        {
            if (_chains == null)
                return context;

            foreach (var chain in _chains)
                _chainedGenerators.AddRange(chain.GetGenerators(context));

            return context;
        }

        public IEnumerable<ICodeGenerator> GetGenerators()
        {
            /* Default type resolver. */
            TypeResolver typeResolver = new TypeResolver();

            /* Test parameters reader uses default strategy. */
            TestParamsFromJsonReader testParamsReader = new TestParamsFromJsonReader(typeResolver);
            UnboundPropertiesFromJsonReader unboundPropertiesReader = new UnboundPropertiesFromJsonReader();

            /* Command execution strategy. */
            IDbCommandExecutionCodeWriter commandExecution = new DataflipCommandExecutionCodeWriter();

            /* Connects to a Sql Database. */
            using (IDbConnectionFactory connectionFactory = new SqlConnectionFactory())
            {
                IMethodBuilder methodBuilder = new SqlCodeMethodBuilder(connectionFactory, typeResolver);
                
                /* Returns an IEnumerable with all the generators. */
                return
                    new ContextSettingsFromJsonReader(
                        _jsonProvider,
                        testParamsReader,
                        unboundPropertiesReader
                    )
                    .ReadContextSettings()
                    .Select(
                        contextSetting => new DbContextCodeGenerator
                        (
                            ChainGeneratorToCodeContext(
                                new CodeContextBuilder(
                                    contextSetting,
                                    typeResolver,
                                    methodBuilder
                                ).Build()
                            ),
                            _output,
                            new ICodeGenerationChunk[]
                            {
                                new UsingChunk(),
                                new NamespaceChunk(
                                    new ClassChunk(
                                        new ResultClassesChunk(),
                                        commandExecution
                                    )
                                )
                            }
                        )
                    )
                    .Union(_chainedGenerators);
            }
        } 
    }
}
