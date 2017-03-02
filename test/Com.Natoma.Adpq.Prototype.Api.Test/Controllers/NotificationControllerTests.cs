using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Api.Controllers;
using Com.Natoma.Adpq.Prototype.Business.Models.Notification;
using Com.Natoma.Adpq.Prototype.Business.Models.Request;
using Com.Natoma.Adpq.Prototype.Business.Services.Interfaces;
using Moq;
using Xunit;

namespace Com.Natoma.Adpq.Prototype.Api.Test.Controllers
{
    public class NotificationControllerTests
    {
        [Fact]
        public void NotificationController_GetNotifications()
        {
            var mockNotificationService = new Mock<INotificationService>();
            mockNotificationService.Setup(x => x.Get())
                .Returns(() => new RequestResult()).Verifiable();
            var controller = new NotificationController(mockNotificationService.Object);

            var result = controller.Get();

            mockNotificationService.Verify(m => m.Get(), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public void NotificationController_GetNotificationById()
        {
            var mockNotificationService = new Mock<INotificationService>();
            mockNotificationService.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(() => new RequestResult()).Verifiable();
            var controller = new NotificationController(mockNotificationService.Object);

            var result = controller.Get(1);

            mockNotificationService.Verify(m => m.Get(It.IsAny<int>()), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async void NotificationController_CreateNotification()
        {
            var mockNotificationService = new Mock<INotificationService>();
            mockNotificationService.Setup(x => x.CreateAndSendNotification(It.IsAny<NotificationViewModel>()))
                .ReturnsAsync(() => new RequestResult()).Verifiable();
            var controller = new NotificationController(mockNotificationService.Object);

            var notification = new NotificationViewModel()
            {
                NotificationType = NotificationTypeEnum.Blast
            };

            var result = await controller.Post(notification);

            mockNotificationService.Verify(m => m.CreateAndSendNotification(It.IsAny<NotificationViewModel>()), Times.Once);
            Assert.NotNull(result);

        }
    }
}
