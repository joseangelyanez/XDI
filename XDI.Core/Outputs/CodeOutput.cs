using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDI.Core.Outputs
{
    public class CodeOutput
    {
        private IEnumerable<ICodeWriter> _writers = null;

        public CodeOutput(IEnumerable<ICodeWriter> writers)
        {
            if (writers == null)
                throw new ArgumentNullException(nameof(writers));

            _writers = writers;
        }

        /// <summary>
        /// The indentation position.
        /// </summary>
        protected int Indentation { get; set; }

        public virtual void BeginCode(string contextName)
        {
            foreach (var writer in _writers)
                writer?.BeginCode(contextName);
        }

        /// <summary>
        /// Increases the indentation.  
        /// </summary>
        private void BeingIndentation()
        {
            Indentation += 1;
        }
        /// <summary>
        /// Decreases the indentation.
        /// </summary>
        private void EndIndentation()
        {
            if (Indentation == 0)
                throw new CodeOutputException("Invalid indentation end, it looks like there's a problem with the code.");

            Indentation -= 1;
        }

        public virtual void Save(string contextName)
        {
            foreach (var writer in _writers)
                writer?.Save(contextName);
        }

        /// <summary>
        /// Writes indented code.
        /// </summary>
        /// <param name="code">The code to write in the line.</param>
        /// <param name="args">The arguments being passed to string.Format within this method.</param>
        public virtual void WriteCode(string code, params object[] args)
        {
            if (code == null)
                throw new ArgumentNullException("code");

            code = args.Length == 0 ?
                    code
                    :
                    string.Format(code, args);

            if (code.StartsWith("}") && !code.StartsWith("{"))
                EndIndentation();

            foreach (var writer in _writers)
                writer?.WriteCode(Indentation, code);

            if (code.StartsWith("{") && !code.EndsWith("}"))
                BeingIndentation();
        }

        public void WriteLine()
        {
            foreach (var writer in _writers)
                writer?.WriteLine();
        }
    }
}
