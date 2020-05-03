using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogEngineAPI.Controllers;
using Api.Application.Contracts.Services;
using Api.Business.Models;
using System.Threading.Tasks;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlogEngineApiTest
{
    [TestClass]
    public class PostsControllerUnitTest
    {
        PostsController _controller;

        public PostsControllerUnitTest()
        {
            var mockervice = new Mock<IPostService>();
            _controller = new PostsController(mockervice.Object);
            
        }

        [TestMethod]
        public async Task CreatePostTestMethod()
        {
            
                Post post = new Post
                {
                    AuhorUserId = 1,
                    Content = @"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium,
                totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit,
                sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt.Neque porro quisquam est,
                qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.Ut enim ad minima veniam,
                quis nostrum exercitationem ullam corporis suscipit laboriosam,
                nisi ut aliquid ex ea commodi consequatur ? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur,
                vel illum qui dolorem eum fugiat quo voluptas nulla pariatur ?"
                };
                

                var actionResult = await _controller.CreatePost(post);

                // Assert
                Assert.IsNotNull(actionResult);
                Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<Post>));
            
        }

        [TestMethod]
        public async Task GetPublishedTestMethod()
        {
            try
            {
                var posts = await _controller.GetPublished();
                foreach(Post item in posts.Value)
                {
                    Assert.AreEqual(item.Status, PostStatus.Approved);
                }
            }
            catch
            {
                Assert.Fail("Unexpected exception");
            }
        }

        [TestMethod]
        public async Task GetRejectedTestMethod()
        {
            try
            {
                User writer = new User { Id=1, Role=UserRole.Writer };
                var posts = await _controller.GetRejected(writer.Id);
                foreach (Post item in posts.Value)
                {
                    Assert.AreEqual(item.Status, PostStatus.Rejected);
                }
            }
            catch
            {
                Assert.Fail("Unexpected exception");
            }
        }

        [TestMethod]
        public async Task GetSubmittedTestMethod()
        {
            try
            {
                var posts = await _controller.GetSubmitted();
                foreach (Post item in posts.Value)
                {
                    Assert.AreEqual(item.Status, PostStatus.Approved);
                }
            }
            catch
            {
                Assert.Fail("Unexpected exception");
            }
        }

        [TestMethod]
        public async Task GetCreatedTestMethod()
        {
            try
            {
                User writer = new User { Id = 1, Role = UserRole.Writer };

                var posts = await _controller.GetCreated(writer.Id);
                foreach (Post item in posts.Value)
                {
                    Assert.AreEqual(item.Status, PostStatus.Approved);
                }
            }
            catch
            {
                Assert.Fail("Unexpected exception");
            }
        }

        [TestMethod]
        public async Task SubmitTestMethod()
        {
            try
            {
                var actionResult = await _controller.Submit(4) as OkObjectResult;

                // Assert
                Assert.IsNotNull(actionResult);
            }
            catch
            {
                Assert.Fail("Unexpected exception");
            }
        }

        [TestMethod]
        public async Task ApproveTestMethod()
        {
            try
            {
                User editor = new User { Id = 2, Role = UserRole.Editor};

                var actionResult = await _controller.Approve(4, editor.Id) as OkObjectResult;

                // Assert
                Assert.IsNotNull(actionResult);
            }
            catch
            {

                Assert.Fail("Unexpected exception");
            }
        }

        [TestMethod]
        public async Task RejectTestMethod()
        {
            try
            {
                User editor = new User { Id = 2, Role = UserRole.Editor };

                var actionResult = await _controller.Reject(4, editor.Id) as OkObjectResult;

                // Assert
                Assert.IsNotNull(actionResult);
            }
            catch
            {
                Assert.Fail("Unexpected exception");
            }
        }
    }
}
