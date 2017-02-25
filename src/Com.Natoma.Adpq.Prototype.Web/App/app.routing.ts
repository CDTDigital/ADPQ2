import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login.component';
import { UserHomeComponent } from './user/user-home.component';
import { UserProfileComponent } from './user/user-profile.component';

const routes: Routes = [
    { path: "", redirectTo: "/user", pathMatch: "full" },
    { path: "home", redirectTo: "/user", pathMatch: "full" },

    { path: "login", component: LoginComponent },
    
    { path: "user/profile", component: UserProfileComponent },
    { path: "user", component: UserHomeComponent }
];
export const routing = RouterModule.forRoot(routes); 