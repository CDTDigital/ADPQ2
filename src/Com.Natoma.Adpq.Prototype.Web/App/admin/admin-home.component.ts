import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from '../user/user.service';
import { ADPQService } from '../shared/adpq.service';
import { MenuItem } from 'primeng/primeng';

@Component({
    templateUrl: '../../html/admin-home.component.html',
    moduleId: module.id,
})
export class AdminHomeComponent implements OnInit {

    constructor(private userService: UserService, private adpqService: ADPQService, private router: Router) { }

    async ngOnInit() {
        if (!this.userService.checkLogin()) {
            this.router.navigate(["./login"]);
            return;
        }

        let user = await this.userService.getLoggedInUser();
        if (user.isAdmin == false)
            this.router.navigate(["./user"]);

        this.adpqService.breadcrumbItems = [<MenuItem> { label: 'Admin Home', routerLink: ['./admin'] }];
    }

}