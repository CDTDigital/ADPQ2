import { NgModule }      from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { SharedModule } from './shared/shared.module';
import { AppComponent } from "./app.component";
import { LoginComponent } from './login.component';

@NgModule({
    imports: [
        BrowserModule,
        SharedModule.forRoot()
    ],
    declarations: [
        AppComponent,
        LoginComponent
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }