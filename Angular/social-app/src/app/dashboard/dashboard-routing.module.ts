import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashHomeComponent } from './dash-home/dash-home.component';


const routes: Routes = [
  {
    path: '',
    component: DashHomeComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
