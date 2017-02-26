import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from '../user/user.service';
import { ADPQService } from '../shared/adpq.service';
import { MenuItem, SelectItem } from 'primeng/primeng';
import { CookieService } from 'angular2-cookie/core';
import { Notification } from '../shared/notification.service';

@Component({
    templateUrl: '../../html/admin-notification.component.html',
    moduleId: module.id,
})
export class AdminNotificationComponent implements OnInit {
    private textWasEnteredIntoEditor: boolean;

    notification: Notification = new Notification();

    notifTypes: SelectItem[] = [
        { value: 0, label: 'Blast' },
        { value: 1, label: 'Regional' }
    ];

    radiuses: SelectItem[] = [
        { value: 20, label: '20' },
        { value: 50, label: '50' },
        { value: 100, label: '100' }
    ];

    constructor(private userService: UserService, private adpqService: ADPQService, private router: Router) { }

    async ngOnInit() {
        if (!this.userService.checkLogin()) {
            this.router.navigate(["./login"]);
            return;
        }

        let user = await this.userService.getLoggedInUser();
        if (user.isAdmin == false)
            this.router.navigate(["./user"]);


        this.adpqService.breadcrumbItems = [
            <MenuItem>{ label: 'Admin Home', routerLink: ['./admin'] },
            <MenuItem>{ label: 'Admin Notification', routerLink: ['./admin/notification'] }
        ];
    }

    onEditorTextChange() {
        this.textWasEnteredIntoEditor = true;
    }
    isTextDisabled() {
        return this.notification.emailMessage || this.notification.emailSubject != '';
    }
}