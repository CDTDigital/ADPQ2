import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login.component';
import { UserHomeComponent } from './user/user-home.component';
import { AdminHomeComponent } from './admin/admin-home.component';
import { UserProfileComponent } from './user/user-profile.component';

const routes: Routes = [
    { path: "", redirectTo: "/user", pathMatch: "full" },
    { path: "home", redirectTo: "/user", pathMatch: "full" },

    { path: "login", component: LoginComponent },
    
    { path: "user/profile", component: UserProfileComponent },
    { path: "user", component: UserHomeComponent },

    { path: "admin/profile", component: UserProfileComponent },
    { path: "admin", component: AdminHomeComponent }
    
];
export const routing = RouterModule.forRoot(routes); 