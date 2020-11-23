import { Component, OnInit } from '@angular/core';
import { Identifiers } from '@angular/compiler/src/render3/r3_identifiers';
import { IDeal } from 'src/app/shared/Interfaces/IDeal';
import { DealService } from '../deal.service';
import { UserService } from 'src/app/user/user.service';

@Component({
  selector: 'app-mine',
  templateUrl: './mine.component.html',
  styleUrls: ['./mine.component.css']
})
export class MineComponent implements OnInit {
  buyDeals: Array<IDeal>
  saleDeals: Array<IDeal>
  constructor(
    private dealService: DealService,
    private userService: UserService
  ) { }

  ngOnInit() {
    this.dealService.getAllDeals(this.userService.getUserId()).subscribe(res => {
      this.buyDeals = res.filter(x => x.isSale === false)
      this.saleDeals = res.filter(x => x.isSale === true)
    })
  }

}
