import { Component, OnInit } from '@angular/core';
import { OfferService } from '../offer.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IOffer } from 'src/app/shared/Interfaces/IOffer';
import { IOfferSeller } from 'src/app/shared/Interfaces/IOfferSeller';
import { UserService } from 'src/app/user/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-all',
  templateUrl: './all.component.html',
  styleUrls: ['./all.component.css']
})
export class AllComponent implements OnInit {

  allOffers: Array<IOfferSeller>
  lisitngTitile: string
  sellerName: string
 
  constructor(
    private offerService: OfferService,
    private activatedRoute: ActivatedRoute,
    private userService: UserService,
    private router: Router,private toastrService: ToastrService) {}

  ngOnInit() {
    this.allOffers = new Array();
    this.offerService.getAllOffersForListing(this.activatedRoute.snapshot.params.id)
    .subscribe(res => {
      this.allOffers = res;
      console.log(this.allOffers);
      this.sellerName = this.allOffers[0].sellerName;
      console.log(this.sellerName);
      
      this.lisitngTitile = this.allOffers[0].title;
    });
    
  }


  acceptOffer(id, listingId, creatorId, price, title){
   
    const dael = { id, listingId, creatorId, price, title, buyerId : this.userService.getUserId()};
    this.offerService.accept(dael).subscribe(res => {
      this.toastrService.success("Offer Accepted")
      this.router.navigate(['listing/all']);
    })
  }

}
