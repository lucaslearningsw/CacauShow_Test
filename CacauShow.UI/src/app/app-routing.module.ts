import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { UserregisterComponent } from './Components/userregister/userregister.component';
import { EditUserComponent } from './Components/edit-user/edit-user.component';

const routes: Routes = [

  {
    path: '', redirectTo:'login', pathMatch: 'full'
  },

  {
    path: 'userregister',
    component:UserregisterComponent
  },
  {
    path:'login',
    component:LoginComponent
  },
  {
    path:'dashboard',
    component:DashboardComponent,
  },
  {
    path: 'user/edit/:id',
    component:EditUserComponent
  }


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
