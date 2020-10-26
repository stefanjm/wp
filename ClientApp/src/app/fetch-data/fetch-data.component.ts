import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public cups: Cups[];
  public sortedCups: SortedCups[];
  private http: HttpClient;
  private baseUrl: string;
 

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.http.get<Cups[]>(this.baseUrl + 'api/cups').subscribe(result => {
      this.cups = result;
    }, error => console.error(error));
  }

  sortCups() {
    this.http.get<SortedCups[]>(this.baseUrl + 'api/cups/sorted').subscribe(result => {
      this.sortedCups = result;
    }, error => console.error(error));
  }
}

interface Cups {
  id: number;
  diameter: number;
  height: number;
}

interface SortedCups {
  id: number;
  sortedCups: Cups;
}
