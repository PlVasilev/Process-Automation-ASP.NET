import { NgForm } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ListingService } from '../listing.service';
import { UserService } from 'src/app/user/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IListing } from 'src/app/shared/Interfaces/IListing';
import { IOffer } from 'src/app/shared/Interfaces/IOffer';
import { OfferService } from 'src/app/offer/offer.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  selectedListing: IListing;
  token: string;
  userId: string;
  offerSize: Number;
  offersCount: Number;
  currnetOffer: IOffer;

  constructor(
    private listingService: ListingService,
    private offerService: OfferService,
    private userService: UserService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private toastrService: ToastrService
  ) { }

  @ViewChild('addOfferForm', { static: true }) from: NgForm

  deleteHandler(id: string) {
    this.listingService.delete(id).subscribe(res => {
      this.router.navigate(['listing/all']);
    })
  }

  addOffertHandler(data) {
    const offerData = {
      price: data['price'],
      creatorId: this.userId,
      listingId: this.selectedListing.id,
      title: this.selectedListing.title,
      creatorName: this.userService.getUsername()
    }
    this.offerService.addOffer(offerData)
      .subscribe(res => {
        this.toastrService.success("Offer Send")
      })
      setTimeout(() => {
        window.location.reload();
    }, 1000);
      
  }

  ngOnInit() {
    this.listingService.details(this.activatedRoute.snapshot.params.id)
      .subscribe(listings => {
        this.listingService.setCurrentListingSeller(listings.sellerId);
        this.selectedListing = listings
        this.userId = this.userService.getUserId()
        this.token = this.userService.getToken()
        const currentOfferData = { creatorId: this.userId, listingId: this.selectedListing.id }
        this.offerService.getCuurentOffer(currentOfferData).subscribe(res => {
          this.offerSize = res;
        })
        this.offerService.getAllOffersForListingCount(this.selectedListing.id).subscribe(res => {
          this.offersCount = res;
        })
      });
  }
}
