import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AllComponent } from './all/all.component';
import { MineComponent } from './mine/mine.component';

@NgModule({
  declarations: [AllComponent, MineComponent],
  imports: [
    CommonModule,
  ],
  exports:[[AllComponent,MineComponent]]
})
export class OfferModule { }
