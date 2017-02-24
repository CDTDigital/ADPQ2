import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login.component';
import { UserHomeComponent } from './user/user-home.component';

const routes: Routes = [
    { path: "", redirectTo: "/user", pathMatch: "full" },
    { path: "home", redirectTo: "/user", pathMatch: "full" },
    { path: "user", component: UserHomeComponent },
    { path: "login", component: LoginComponent }
];
export const routing = RouterModule.forRoot(routes); 