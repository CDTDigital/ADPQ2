import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { User } from './user.service';
import { NotificationService, Notification } from '../shared/notification.service';
import { UserService } from './user.service';
import { ADPQService } from '../shared/adpq.service';
import { MenuItem } from 'primeng/primeng';

@Component({
    templateUrl: '../../html/user-notifications.component.html',
    moduleId: module.id,
})
export class UserNotificationsComponent implements OnInit {
    notifications: Notification[];

    constructor(private userService: UserService, private adpqService: ADPQService, private router: Router, private notificationService: NotificationService) { }

    async ngOnInit() {
        if (!this.userService.checkLogin()) {
            this.router.navigate(["./login"]);
            return;
        }

        let user = await this.userService.getLoggedInUser();
        if (user && user.isAdmin == true)
            this.router.navigate(["./admin"]);

        this.adpqService.breadcrumbItems = [<MenuItem>{ label: 'User Home', routerLink: ['./user'] }, <MenuItem>{ label: 'User Notifications', routerLink: ['./user/notifications'] }];

        this.notifications = await this.notificationService.getNotificationsForUser(user.userProfileId);
    }

}