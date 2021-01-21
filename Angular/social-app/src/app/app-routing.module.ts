import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  { path: '', loadChildren: ()=> import('./auth/auth.module').then(x=>x.AuthModule)},
  { path: 'dashboard', loadChildren: ()=> import('./dashboard/dashboard.module').then(x=>x.DashboardModule)},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
