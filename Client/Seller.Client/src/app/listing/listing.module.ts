import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AddComponent } from './add/add.component';
import { ListingRoutingModule } from './listing-routing.module';
import { AllComponent } from './all/all.component';
import { DetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';
import { MineComponent } from './mine/mine.component';


@NgModule({
  declarations: [AddComponent, AllComponent, DetailsComponent, EditComponent, MineComponent],
  imports: [
    CommonModule,
    FormsModule,
    ListingRoutingModule
  ],exports:[
    [AddComponent,AllComponent,DetailsComponent, EditComponent, MineComponent]],
})
export class ListingModule { }
