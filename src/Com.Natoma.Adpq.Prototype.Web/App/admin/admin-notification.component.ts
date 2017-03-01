import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from '../user/user.service';
import { ADPQService } from '../shared/adpq.service';
import { MenuItem, SelectItem } from 'primeng/primeng';
import { CookieService } from 'angular2-cookie/core';
import { Notification, NotificationService } from '../shared/notification.service';
import { StatesFactory } from '../shared/states';

@Component({
    templateUrl: '../../html/admin-notification.component.html',
    moduleId: module.id,
})
export class AdminNotificationComponent implements OnInit {
    private textWasEnteredIntoEditor: boolean;

    selectedStateIdx: number = 4;
    states: SelectItem[] = StatesFactory.getStatesAsSelectItems();

    notification: Notification = new Notification();

    notifTypes: SelectItem[] = [
        { value: 1, label: 'Blast' },
        { value: 2, label: 'Regional' }
    ];

    radiuses: SelectItem[] = [
        { value: 20, label: '20' },
        { value: 50, label: '50' },
        { value: 100, label: '100' }
    ];

    constructor(private userService: UserService, private adpqService: ADPQService, private router: Router,
        private notificationService: NotificationService) { }

    async ngOnInit() {
        if (!this.userService.checkLogin()) {
            this.router.navigate(["./login"]);
            return;
        }

        let user = await this.userService.getLoggedInUser();
        if (user && user.isAdmin == false)
            this.router.navigate(["./user"]);


        this.adpqService.breadcrumbItems = [
            <MenuItem>{ label: 'Admin Home', routerLink: ['./admin'] },
            <MenuItem>{ label: 'Admin Notification', routerLink: ['./admin/notification'] }
        ];
    }

    onEditorTextChange() {
        this.textWasEnteredIntoEditor = true;
    }

    async onSubmit(save = false) {
        if (save) {
            this.notification.state = StatesFactory.getStates()[this.selectedStateIdx].shortName;
            let res = await this.notificationService.postNotification(this.notification);
            if (res) {
                this.adpqService.growl({ severity: 'success', summary: `Notification request was successful` });
                this.router.navigate(['./admin']);
            }
        }
        else
            this.router.navigate(['./admin']);

    }

    onNotifTypeDropdownChange() {
        // Working around a PrimeNG bug
        let tooltip = document.querySelector(".ui-widget.ui-tooltip.ui-tooltip-right");
        tooltip.parentNode.removeChild(tooltip);
    }
}