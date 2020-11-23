import { Injectable } from '@angular/core';
import { IDeal } from '../shared/Interfaces/IDeal';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DealService {

  constructor(private http: HttpClient) { }

  private getAllMineDealsGatewayPath = environment.listingGatewayApiUrl + 'Deal/Mine/'

  getAllDeals(id): Observable<Array<IDeal>> {
    return this.http.get<Array<IDeal>>(this.getAllMineDealsGatewayPath + id)
  }
}

