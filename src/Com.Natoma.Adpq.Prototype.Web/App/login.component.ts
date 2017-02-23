import { Component, OnInit } from '@angular/core';
import { User } from './user.service';
import { AuthService } from './shared/auth.service';
import { UserService } from './user.service';
import { ADPQService, GrowlObject } from './shared/adpq.service';
import { Message } from 'primeng/primeng';

@Component({
    selector: 'adpq-login',
    templateUrl: '../html/login.component.html',
    styleUrls: ['../css/login.component.css'],
    moduleId: module.id,
})
export class LoginComponent implements OnInit {

    user: User = new User();
    newUser: User = new User();

    isSignupVisible = false;

    constructor(private authService: AuthService, private userService: UserService, private adpqService: ADPQService) { }

    ngOnInit() {
    }

    doMakeSignupVisible() {
        this.user = new User();
        this.isSignupVisible = true;
    }

    doLogin() {
        this.authService.login(this.user.email, this.user.password)
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

    async doSignup(save: boolean) {
        if (!save)
            this.newUser = new User();
        else {
            let res = await this.userService.create(this.newUser);
            debugger;
        }
    }
}