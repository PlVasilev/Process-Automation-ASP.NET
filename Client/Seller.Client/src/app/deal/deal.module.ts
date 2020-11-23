import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MineComponent } from './mine/mine.component';
import { DealRoutingModule } from './deal-routing.module';



@NgModule({
  declarations: [MineComponent],
  imports: [
    CommonModule,
    DealRoutingModule
  ], exports:[[MineComponent]]
})
export class DealModule { }
