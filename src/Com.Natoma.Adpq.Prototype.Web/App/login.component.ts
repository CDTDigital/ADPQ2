import { Component } from '@angular/core';
import { User } from './user/user.service';
import { UserService } from './user/user.service';
import { ADPQService, RequestStateEnum, RequestResult } from './shared/adpq.service';
import { Message } from 'primeng/primeng';
import { StatesFactory } from './shared/states';
import { Router } from '@angular/router';

@Component({
    selector: 'adpq-login',
    templateUrl: '../html/login.component.html',
    moduleId: module.id,
})
export class LoginComponent {

    infoMessages: Message[] = [];
    isSignupVisible = false;
    loginUser: User = new User();
    newUser: User = new User();

    constructor(private userService: UserService, private adpqService: ADPQService, private router: Router) { }

    doMakeSignupVisible() {
        this.newUser = new User();
        this.isSignupVisible = true;
    }

    doLogin() {
        this.userService.login(this.loginUser.email, this.loginUser.password)
            .then((result: RequestResult) => {
                if (result.state == RequestStateEnum.SUCCESS) {
                    setTimeout(() => {
                        this.routeUser(result.data.isAdmin);
                    }, 200);
                }
                else {
                    this.infoMessages.push({ severity: 'error', summary: `Error logging in.`, detail: result.msg });
                }
            });
    }

    private routeUser(isAdmin: boolean) {
        if (isAdmin)
            this.router.navigate(["./admin"]);
        else
            this.router.navigate(["./user"]);
    }

    async onSignupFormSubmit(event) {
        if (event && event.user) {
            event.user.state = StatesFactory.getStates()[event.selectedStateIdx].shortName;
            let userRes = await this.userService.create(event.user);
            if (userRes) {
                this.adpqService.growl({ severity: 'success', summary: `Thank you ${event.user.firstName}! You can now log in.`, detail: 'User successfully created' });
                this.newUser = new User();

                this.routeUser(userRes.isAdmin);
            }
        }
        this.isSignupVisible = false;
    }
}