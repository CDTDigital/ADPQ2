﻿import { NgModule }      from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { SharedModule } from './shared/shared.module';
import { AppComponent } from "./app.component";
import { LoginComponent } from './login.component';
import { UserHomeComponent } from './user/user-home.component';
import { UserFormComponent } from './user/user-form.component';
import { UserProfileComponent } from './user/user-profile.component';
import { AdminHomeComponent } from './admin/admin-home.component';
import { AdminNotificationComponent } from './admin/admin-notification.component';


import { routing } from './app.routing';

@NgModule({
    imports: [
        BrowserModule,
        routing,
        SharedModule.forRoot()
    ],
    declarations: [
        AppComponent,
        LoginComponent,
        UserHomeComponent,
        UserFormComponent,
        UserProfileComponent,
        AdminHomeComponent,
        AdminNotificationComponent
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }