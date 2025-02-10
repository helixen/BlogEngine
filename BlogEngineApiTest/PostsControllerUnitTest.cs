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
        Mock<IPostService> mockService;

        public PostsControllerUnitTest()
        {
            mockService = new Mock<IPostService>();
            _controller = new PostsController(mockService.Object);
            
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
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
            var okObjectResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            Assert.IsInstanceOfType(okObjectResult.Value, typeof(Post));
            Assert.AreEqual(post, okObjectResult.Value);
        }

        [TestMethod]
        public async Task GetPublishedTestMethod()
        {
            // Arrange
            var expectedPosts = new List<Post>
            {
                new Post { Id = 1, Status = PostStatus.Approved, Content = "Post 1", AuhorUserId = 1 },
                new Post { Id = 2, Status = PostStatus.Approved, Content = "Post 2", AuhorUserId = 1 }
            };
            mockService.Setup(service => service.GetPublishedPosts()).ReturnsAsync(expectedPosts);
            _controller = new PostsController(mockService.Object);

            // Act
            var actionResult = await _controller.GetPublished();

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
            var okObjectResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            var posts = okObjectResult.Value as IEnumerable<Post>;
            Assert.IsNotNull(posts);
            foreach (Post item in posts)
            {
                Assert.AreEqual(PostStatus.Approved, item.Status);
            }
        }

        [TestMethod]
        public async Task GetRejectedTestMethod()
        {
            // Arrange
            var writer = new User { Id = 1, Role = UserRole.Writer };
            var expectedPosts = new List<Post>
            {
                new Post { Id = 1, Status = PostStatus.Rejected, Content = "Post 1", AuhorUserId = writer.Id },
                new Post { Id = 2, Status = PostStatus.Rejected, Content = "Post 2", AuhorUserId = writer.Id }
            };
            mockService.Setup(service => service.GetRejectedPosts(writer.Id)).ReturnsAsync(expectedPosts);
            _controller = new PostsController(mockService.Object);

            // Act
            var actionResult = await _controller.GetRejected(writer.Id);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
            var okObjectResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            var posts = okObjectResult.Value as IEnumerable<Post>;
            Assert.IsNotNull(posts);
            foreach (Post item in posts)
            {
                Assert.AreEqual(PostStatus.Rejected, item.Status);
            }
        }

        [TestMethod]
        public async Task GetSubmittedTestMethod()
        {
            // Arrange
            var writer = new User { Id = 1, Role = UserRole.Editor };
            var expectedPosts = new List<Post>
            {
                new Post { Id = 1, Status = PostStatus.Submitted, Content = "Post 1", AuhorUserId = writer.Id },
                new Post { Id = 2, Status = PostStatus.Submitted, Content = "Post 2", AuhorUserId = writer.Id }
            };
            mockService.Setup(service => service.GetSubmittedPosts()).ReturnsAsync(expectedPosts);
            _controller = new PostsController(mockService.Object);

            // Act
            var actionResult = await _controller.GetSubmitted();

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
            var okObjectResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            var posts = okObjectResult.Value as IEnumerable<Post>;
            Assert.IsNotNull(posts);
            foreach (Post item in posts)
            {
                Assert.AreEqual(PostStatus.Submitted, item.Status);
            }
        }

        [TestMethod]
        public async Task GetCreatedTestMethod()
        {
            // Arrange
            var writer = new User { Id = 1, Role = UserRole.Writer };
            var expectedPosts = new List<Post>
            {
                new Post { Id = 1, Status = PostStatus.Created, Content = "Post 1", AuhorUserId = writer.Id },
                new Post { Id = 2, Status = PostStatus.Created, Content = "Post 2", AuhorUserId = writer.Id }
            };
            mockService.Setup(service => service.GetCreatedPosts(writer.Id)).ReturnsAsync(expectedPosts);
            _controller = new PostsController(mockService.Object);

            // Act
            var actionResult = await _controller.GetCreated(writer.Id);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
            var okObjectResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            var posts = okObjectResult.Value as IEnumerable<Post>;
            Assert.IsNotNull(posts);
            foreach (Post item in posts)
            {
                Assert.AreEqual(PostStatus.Created, item.Status);
            }
        }

        [TestMethod]
        public async Task SubmitTestMethod()
        {
            // Arrange
            var postId = 4;
            var expectedPost = new Post { Id = postId, Status = PostStatus.Submitted, Content = "Post Content", AuhorUserId = 1 };
            mockService.Setup(service => service.UpdateStatus(postId, PostStatus.Submitted, null)).ReturnsAsync(expectedPost);
            _controller = new PostsController(mockService.Object);

            // Act
            var actionResult = await _controller.Submit(postId);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public async Task ApproveTestMethod()
        {
            // Arrange
            var postId = 4;
            var editor = new User { Id = 2, Role = UserRole.Editor };
            var expectedPost = new Post { Id = postId, Status = PostStatus.Approved, Content = "Post Content", AuhorUserId = 1 };
            mockService.Setup(service => service.UpdateStatus(postId, PostStatus.Approved, editor.Id)).ReturnsAsync(expectedPost);
            _controller = new PostsController(mockService.Object);

            // Act
            var actionResult = await _controller.Approve(postId, editor.Id);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public async Task RejectTestMethod()
        {
            // Arrange
            var postId = 4;
            var editor = new User { Id = 2, Role = UserRole.Editor };
            var expectedPost = new Post { Id = postId, Status = PostStatus.Rejected, Content = "Post Content", AuhorUserId = 1 };
            mockService.Setup(service => service.UpdateStatus(postId, PostStatus.Rejected, editor.Id)).ReturnsAsync(expectedPost);
            _controller = new PostsController(mockService.Object);

            // Act
            var actionResult = await _controller.Reject(postId, editor.Id);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }
    }
}
