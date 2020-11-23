import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { IOffer } from '../shared/Interfaces/IOffer';
import { IOfferSeller } from '../shared/Interfaces/IOfferSeller';

@Injectable({
  providedIn: 'root'
})
export class OfferService {

  constructor(private http: HttpClient) { }

  private addPath = environment.offersApiUrl + 'offer/add'
  private getCurrentPath = environment.offersApiUrl + 'offer/getCurrentOffer'
  private getAllOffersForListingCountPath = environment.offersApiUrl + 'offer/count/'
  private getAllMineOffersForListingPath = environment.offersApiUrl + 'offer/mine/'
  private getDeleteOffersForListingPath = environment.offersApiUrl + 'offer/DeleteOffer/'
  private getAllOffersForListingCountGatewayPath = environment.listingGatewayApiUrl + 'Listing/OffersAll/'
  private crateDealPAth = environment.listingGatewayApiUrl + 'Listing/Deal'

  addOffer(data): Observable<IOffer> {
    return this.http.post<IOffer>(this.addPath, data)
  }

  getCuurentOffer(data): Observable<Number> {
    return this.http.post<Number>(this.getCurrentPath, data)
  }

  getAllOffersForListing(id): Observable<Array<IOfferSeller>> {
  return this.http.get<Array<IOfferSeller>>(this.getAllOffersForListingCountGatewayPath + id)

  }

  getAllOffersForListingCount(id): Observable<Number> {
    return this.http.get<Number>(this.getAllOffersForListingCountPath + id)
  }

  getAllMineOffers(id): Observable<Array<IOfferSeller>> {
    return this.http.get<Array<IOfferSeller>>(this.getAllMineOffersForListingPath + id)
  }

  deleteOffer(id): Observable<boolean> {
    return this.http.delete<boolean>(this.getDeleteOffersForListingPath + id)
  }

  accept(model) {
    console.log(this.crateDealPAth);

    return this.http.post(this.crateDealPAth, model)
  }
}
