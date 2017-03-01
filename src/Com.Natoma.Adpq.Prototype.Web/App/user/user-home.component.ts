import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { User } from './user.service';
import { UserService } from './user.service';
import { ADPQService } from '../shared/adpq.service';
import { MenuItem } from 'primeng/primeng';

@Component({
    templateUrl: '../../html/user-home.component.html',
    moduleId: module.id,
})
export class UserHomeComponent implements OnInit {

    constructor(private userService: UserService, private adpqService: ADPQService, private router: Router) { }

    async ngOnInit() {
        if (!this.userService.checkLogin()) {
            this.router.navigate(["./login"]);
            return;
        }

        let user = await this.userService.getLoggedInUser();
        if (user && user.isAdmin == true)
            this.router.navigate(["./admin"]);

        this.adpqService.breadcrumbItems = [<MenuItem> { label: 'User Home', routerLink: ['./user'] }];
    }

}