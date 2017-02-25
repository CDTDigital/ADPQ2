import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { User } from './user.service';
import { AuthService } from '../shared/auth.service';
import { UserService } from './user.service';
import { ADPQService, GrowlObject, RequestResult } from '../shared/adpq.service';
import { Message } from 'primeng/primeng';
import { CookieService } from 'angular2-cookie/core';

@Component({
    templateUrl: '../../html/user-home.component.html',
    moduleId: module.id,
})
export class UserHomeComponent implements OnInit {
    isLogin = false;
    userName: string;

    constructor(private authService: AuthService, private userService: UserService, private adpqService: ADPQService,
        private router: Router, private cookieService: CookieService) { }

    async ngOnInit() {
        this.isLogin = this.authService.checkLogin();
        if (this.isLogin) {
            try {
                setTimeout(async () => {
                    this.userService.loggedInUser = await this.authService.getUserInfo(parseInt(this.cookieService.get(AuthService.userIdSessionKey)));
                }, 0);
            } 
            catch (e) {
                this.router.navigate(["./login"]);
            }
        }
        else
            this.router.navigate(["./login"]);
    }

}