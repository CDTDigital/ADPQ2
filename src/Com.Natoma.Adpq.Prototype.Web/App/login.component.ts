import { Component, OnInit } from '@angular/core';
import { User } from './user/user.service';
import { AuthService } from './shared/auth.service';
import { UserService } from './user/user.service';
import { ADPQService, GrowlObject, RequestStateEnum, RequestResult } from './shared/adpq.service';
import { Message } from 'primeng/primeng';
import { StatesFactory } from './shared/states';
import { Router } from '@angular/router';

@Component({
    selector: 'adpq-login',
    templateUrl: '../html/login.component.html',
    styleUrls: ['../css/login.component.css'],
    moduleId: module.id,
})
export class LoginComponent implements OnInit {

    infoMessages: Message[] = [];
    isSignupVisible = false;
    loginUser: User = new User();
    newUser: User = new User();

    constructor(private authService: AuthService, private userService: UserService, private adpqService: ADPQService, private router: Router) { }

    ngOnInit() {
    }

    doMakeSignupVisible() {
        this.newUser = new User();
        this.isSignupVisible = true;
    }

    doLogin() {
        this.authService.login(this.loginUser.email, this.loginUser.password)
            .then(result => {
                if (result.state == RequestStateEnum.SUCCESS) {
                    this.router.navigate(["./home"]);
                }
                else {
                    this.infoMessages.push({ severity: 'error', summary: `Error logging in.`, detail: result.msg });
                }
            });
    }

    async onSignupFormSubmit(event) {
        if (event.user) {
            event.user.state = StatesFactory.getStates()[event.selectedStateIdx].shortName;
            let userRes = await this.userService.create(event.user);
            this.adpqService.growl({ severity: 'success', summary: `Thank you ${event.user.firstName}! You can now log in.`, detail: 'User successfully created' });
            event.user = new User();
        }
        this.isSignupVisible = false;
    }
}