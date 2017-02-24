import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { User } from './user.service';
import { AuthService } from '../shared/auth.service';
import { UserService } from './user.service';
import { ADPQService, GrowlObject, RequestResult } from '../shared/adpq.service';
import { Message } from 'primeng/primeng';

@Component({
    templateUrl: '../../html/user-home.component.html',
    styleUrls: ['../../css/user-home.component.css'],
    moduleId: module.id,
})
export class UserHomeComponent implements OnInit {
    isLogin = false;
    userName: string;

    constructor(private authService: AuthService, private userService: UserService, private adpqService: ADPQService,
        private router: Router) { }

    async ngOnInit() {
        this.isLogin = this.authService.checkLogin();
        if (this.isLogin) {
            let res: RequestResult;
            try {
                res = await this.authService.getUserInfo();
                this.userName = (res.data as any).UserName;
            }
            catch (e) {
                this.router.navigate(["./login"]);
            }
        }
        else
            this.router.navigate(["./login"]);
    }

}