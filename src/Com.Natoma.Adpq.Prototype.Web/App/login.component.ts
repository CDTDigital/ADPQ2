import { Component, OnInit } from '@angular/core';
import { User } from './user/user.service';
import { AuthService } from './shared/auth.service';
import { UserService } from './user/user.service';
import { ADPQService, GrowlObject } from './shared/adpq.service';
import { Message } from 'primeng/primeng';
import { StatesFactory } from './shared/states';

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

    constructor(private authService: AuthService, private userService: UserService, private adpqService: ADPQService) { }

    ngOnInit() {
    }

    doMakeSignupVisible() {
        this.isSignupVisible = true;
    }

    doLogin() {
        this.authService.login(this.loginUser.email, this.loginUser.password)
            .then(result => {
                if (result.State == 1) {
                    //this.router.navigate(["./home"]);
                    console.log("OK!");
                }
                else {
                    this.adpqService.growl(<Message>{ summary: result.Msg, detail: "Server Error", severity: "error" });
                }
            });
    }

    async onSignupFormSubmit(event) {
        if (event.user) {
            event.user.state = StatesFactory.getStates()[event.selectedStateIdx].shortName;
            let userRes = await this.userService.create(event.user);
            this.infoMessages.push({ severity: 'success', summary: `Thank you ${event.user.firstName}! You can now log in.`, detail: 'User successfully created' });
            event.user = new User();
        }
        this.isSignupVisible = false;
    }
}