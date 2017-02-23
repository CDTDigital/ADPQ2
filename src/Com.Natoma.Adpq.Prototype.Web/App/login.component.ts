import { Component, OnInit } from '@angular/core';
import { User } from './user.service';

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

    ngOnInit() { 
    }

    doMakeSignupVisible() {
        this.newUser = new User();
        this.isSignupVisible = true;
    }

    doLogin() {
        console.log("doLogin");
    }
}