using System;
using System.Linq;
using Dataflip;
using System.Collections.Generic;

namespace Zunwave.Data
{
	public class Context : DataflipContext
	{
		public Context(DataflipSettings settings)
		: 
		base(settings)
		{}

		public Context()
		: 
		base("Context")
		{}
		#region GetActivities
		///<summary>
		///Gets all the activities
		///</summary>
		public IEnumerable<GetActivitiesResult> GetActivities(GetActivitiesParameters parameters)
		{
			return new SqlQuery(Settings).ExecuteObjectArray(
			query : "GetActivities",
			parameters : _ =>
			{
				if (parameters == null) return;

				_.AddWithValue("@RETURN_VALUE", parameters.RETURN_VALUE);
				_.AddWithValue("@Name", parameters.Name);
			},
			mapping: reader => new GetActivitiesResult()
			{
				ActivityId = reader["ActivityId"] as Int32,
				Content = reader["Content"] as String,
				ActivityType = reader["ActivityType"] as String,
				ActivityImage = reader["ActivityImage"] as String,
				CreatedDate = reader["CreatedDate"] as DateTime,
				Audience = reader["Audience"] as String,
				SomeCoolDescription = reader["SomeCoolDescription"] as String,
				Name = reader["Name"] as String,
				SmallImageUrl = reader["SmallImageUrl"] as String,
				LargeImageUrl = reader["LargeImageUrl"] as String
			}
			);
		}
		#endregion

		#region GetBooks
		public IEnumerable<GetBooksResult> GetBooks(GetBooksParameters parameters)
		{
			return new SqlQuery(Settings).ExecuteObjectArray(
			query : "GetBooks",
			parameters : _ =>
			{
				if (parameters == null) return;

				_.AddWithValue("@RETURN_VALUE", parameters.RETURN_VALUE);
				_.AddWithValue("@AppUserId", parameters.AppUserId);
			},
			mapping: reader => new GetBooksResult()
			{
				AppUserId = reader["AppUserId"] as Int32,
				BookId = reader["BookId"] as Int32,
				Title = reader["Title"] as String,
				Author = reader["Author"] as String,
				Comment = reader["Comment"] as String
			}
			);
		}
		#endregion

		#region GetArticles
		public IEnumerable<GetArticlesResult> GetArticles(GetArticlesParameters parameters)
		{
			return new SqlQuery(Settings).ExecuteObjectArray(
			query : "GetArticles",
			parameters : _ =>
			{
				if (parameters == null) return;

				_.AddWithValue("@RETURN_VALUE", parameters.RETURN_VALUE);
				_.AddWithValue("@AppUserId", parameters.AppUserId);
			},
			mapping: reader => new GetArticlesResult()
			{
				ArticleId = reader["ArticleId"] as Int32,
				AppUserId = reader["AppUserId"] as Int32,
				Title = reader["Title"] as String,
				ShortDescription = reader["ShortDescription"] as String,
				Content = reader["Content"] as String
			}
			);
		}
		#endregion

		#region GetCodeProjects
		public IEnumerable<GetCodeProjectsResult> GetCodeProjects(GetCodeProjectsParameters parameters)
		{
			return new SqlQuery(Settings).ExecuteObjectArray(
			query : "GetCodeProjects",
			parameters : _ =>
			{
				if (parameters == null) return;

				_.AddWithValue("@RETURN_VALUE", parameters.RETURN_VALUE);
				_.AddWithValue("@AppUserId", parameters.AppUserId);
			},
			mapping: reader => new GetCodeProjectsResult()
			{
				ProjectId = reader["ProjectId"] as Int32,
				AppUserId = reader["AppUserId"] as Int32,
				Url = reader["Url"] as String,
				Name = reader["Name"] as String,
				Description = reader["Description"] as String,
				ProjectType = reader["ProjectType"] as String
			}
			);
		}
		#endregion

		#region 
		public int (Parameters parameters)
		{
			return new SqlQuery(Settings).ExecuteNonQuery(
			query : "",
			parameters : _ =>
			{
				if (parameters == null) return;

				_.AddWithValue("@RETURN_VALUE", parameters.RETURN_VALUE);
				_.AddWithValue("@AppUserId", parameters.AppUserId);
				_.AddWithValue("@Url", parameters.Url);
				_.AddWithValue("@Name", parameters.Name);
				_.AddWithValue("@Description", parameters.Description);
				_.AddWithValue("@ProjectType", parameters.ProjectType);
			}
			);
		}
		#endregion

		#region 
		public int (Parameters parameters)
		{
			return new SqlQuery(Settings).ExecuteNonQuery(
			query : "",
			parameters : _ =>
			{
				if (parameters == null) return;

				_.AddWithValue("@RETURN_VALUE", parameters.RETURN_VALUE);
				_.AddWithValue("@AppUserId", parameters.AppUserId);
				_.AddWithValue("@Title", parameters.Title);
				_.AddWithValue("@Author", parameters.Author);
				_.AddWithValue("@Comment", parameters.Comment);
			}
			);
		}
		#endregion

		#region 
		public int (Parameters parameters)
		{
			return new SqlQuery(Settings).ExecuteNonQuery(
			query : "",
			parameters : _ =>
			{
				if (parameters == null) return;

				_.AddWithValue("@RETURN_VALUE", parameters.RETURN_VALUE);
				_.AddWithValue("@AppUserId", parameters.AppUserId);
				_.AddWithValue("@Title", parameters.Title);
				_.AddWithValue("@ShortDescription", parameters.ShortDescription);
				_.AddWithValue("@Content", parameters.Content);
			}
			);
		}
		#endregion

		#region GetSocialNetworks
		public IEnumerable<GetSocialNetworksResult> GetSocialNetworks(GetSocialNetworksParameters parameters)
		{
			return new SqlQuery(Settings).ExecuteObjectArray(
			query : "GetSocialNetworks",
			parameters : _ =>
			{
				if (parameters == null) return;

				_.AddWithValue("@RETURN_VALUE", parameters.RETURN_VALUE);
				_.AddWithValue("@AppUserId", parameters.AppUserId);
			},
			mapping: reader => new GetSocialNetworksResult()
			{
				SocialNetworkId = reader["SocialNetworkId"] as Int32,
				Name = reader["Name"] as String,
				SmallIcon = reader["SmallIcon"] as String,
				Url = reader["Url"] as String
			}
			);
		}
		#endregion

		#region GetTechnologies
		public IEnumerable<GetTechnologiesResult> GetTechnologies(GetTechnologiesParameters parameters)
		{
			return new SqlQuery(Settings).ExecuteObjectArray(
			query : "GetTechnologies",
			parameters : _ =>
			{
				if (parameters == null) return;

				_.AddWithValue("@RETURN_VALUE", parameters.RETURN_VALUE);
				_.AddWithValue("@AppUserId", parameters.AppUserId);
			},
			mapping: reader => new GetTechnologiesResult()
			{
				TechnologyId = reader["TechnologyId"] as Int32,
				Name = reader["Name"] as String,
				Type = reader["Type"] as String
			}
			);
		}
		#endregion

		#region GetAppUser
		public IEnumerable<GetAppUserResult> GetAppUser(GetAppUserParameters parameters)
		{
			return new SqlQuery(Settings).ExecuteObjectArray(
			query : "GetAppUser",
			parameters : _ =>
			{
				if (parameters == null) return;

				_.AddWithValue("@RETURN_VALUE", parameters.RETURN_VALUE);
				_.AddWithValue("@AppUserId", parameters.AppUserId);
			},
			mapping: reader => new GetAppUserResult()
			{
				AppUserId = reader["AppUserId"] as Int32,
				Email = reader["Email"] as String,
				Name = reader["Name"] as String,
				ProfileImageUrl = reader["ProfileImageUrl"] as String,
				ScreenName = reader["ScreenName"] as String
			}
			);
		}
		#endregion

		#region 
		public int (Parameters parameters)
		{
			return new SqlQuery(Settings).ExecuteNonQuery(
			query : "",
			parameters : _ =>
			{
				if (parameters == null) return;

				_.AddWithValue("@RETURN_VALUE", parameters.RETURN_VALUE);
				_.AddWithValue("@Email", parameters.Email);
				_.AddWithValue("@Name", parameters.Name);
				_.AddWithValue("@ScreenName", parameters.ScreenName);
			}
			);
		}
		#endregion

		#region Result Classes
		public class GetActivitiesResult
		{
			public Int32 ActivityId { get; set; }
			public String Content { get; set; }
			public String ActivityType { get; set; }
			public String ActivityImage { get; set; }
			public DateTime CreatedDate { get; set; }
			public String Audience { get; set; }
			public String SomeCoolDescription { get; set; }
			public String Name { get; set; }
			public String SmallImageUrl { get; set; }
			public String LargeImageUrl { get; set; }
		}

		public class GetBooksResult
		{
			public Int32 AppUserId { get; set; }
			public Int32 BookId { get; set; }
			public String Title { get; set; }
			public String Author { get; set; }
			public String Comment { get; set; }
		}

		public class GetArticlesResult
		{
			public Int32 ArticleId { get; set; }
			public Int32 AppUserId { get; set; }
			public String Title { get; set; }
			public String ShortDescription { get; set; }
			public String Content { get; set; }
		}

		public class GetCodeProjectsResult
		{
			public Int32 ProjectId { get; set; }
			public Int32 AppUserId { get; set; }
			public String Url { get; set; }
			public String Name { get; set; }
			public String Description { get; set; }
			public String ProjectType { get; set; }
		}

		public class GetSocialNetworksResult
		{
			public Int32 SocialNetworkId { get; set; }
			public String Name { get; set; }
			public String SmallIcon { get; set; }
			public String Url { get; set; }
		}

		public class GetTechnologiesResult
		{
			public Int32 TechnologyId { get; set; }
			public String Name { get; set; }
			public String Type { get; set; }
		}

		public class GetAppUserResult
		{
			public Int32 AppUserId { get; set; }
			public String Email { get; set; }
			public String Name { get; set; }
			public String ProfileImageUrl { get; set; }
			public String ScreenName { get; set; }
		}

		#endregion

	}

}
