import { NgModule }      from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { SharedModule } from './shared/shared.module';
import { AppComponent } from "./app.component";
import { LoginComponent } from './login.component';
import { UserHomeComponent } from './user/user-home.component';
import { UserFormComponent } from './user/user-form.component';

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
        UserFormComponent
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }