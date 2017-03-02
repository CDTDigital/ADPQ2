import { Component, OnInit } from '@angular/core';
import { User } from './user/user.service';
import { UserService } from './user/user.service';
import { ADPQService, RequestStateEnum, RequestResult } from './shared/adpq.service';
import { Message } from 'primeng/primeng';
import { StatesFactory } from './shared/states';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/primeng';

@Component({
    templateUrl: '../html/faq.component.html',
    moduleId: module.id,
})
export class FaqComponent implements OnInit {
    private isAdmin: boolean;

    constructor(private userService: UserService, private adpqService: ADPQService) { }

    async ngOnInit() {
        this.isAdmin = (await this.userService.getLoggedInUser()).isAdmin;

        this.adpqService.breadcrumbItems = [
            <MenuItem>{ label: 'FAQ', routerLink: ['./faq'] }
        ];
    }
}