import { Component, OnInit } from '@angular/core';
import { IOfferSeller } from 'src/app/shared/Interfaces/IOfferSeller';
import { OfferService } from '../offer.service';
import { UserService } from 'src/app/user/user.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-mine',
  templateUrl: './mine.component.html',
  styleUrls: ['./mine.component.css']
})
export class MineComponent implements OnInit {

  allOffers: Array<IOfferSeller>
  lisitngTitile: string
  sellerName: string
 
  constructor(
    private offerService: OfferService,
    private userService: UserService,
    private router: Router,
    private toastrService: ToastrService) {}

  ngOnInit() {
    this.allOffers = new Array();
    this.offerService.getAllMineOffers(this.userService.getUserId())
    .subscribe(res => {
      this.allOffers = res;
      console.log(this.allOffers);
    });
  }

  details(id){
    this.router.navigate([`/listing/details/${id}`])
  }

  deleteOffer(id){
    this.offerService.deleteOffer(id).subscribe(res => {
      this.toastrService.success("Offer Removed")
      window.location.reload();
    })
  }

}
