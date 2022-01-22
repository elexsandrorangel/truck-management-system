import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import { TruckFormComponent } from './components/truck-form/truck-form.component';
import { TrucksComponent } from './components/trucks/trucks.component';

const routes: Routes = [
  {path: '', redirectTo: 'trucks', pathMatch: 'full'},
  {path: 'trucks', component: TrucksComponent},
  {path: 'trucks/add', component: TruckFormComponent},
  {path: 'trucks/:id', component: TruckFormComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}