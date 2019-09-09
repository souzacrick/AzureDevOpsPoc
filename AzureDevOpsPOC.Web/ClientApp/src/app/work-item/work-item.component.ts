import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-work-item',
  templateUrl: './work-item.component.html'
})

export class WorkItemComponent {
  public typeFilter: string = "";
  public workItems: WorkItem[];
  private ascending: boolean = true;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<WorkItem[]>(baseUrl + 'api/WorkItem/GetAll').subscribe(result => {
      this.workItems = result;
    }, error => console.error(error));
  }

  onFilter(event: KeyboardEvent) {
    this.typeFilter = (<HTMLInputElement>event.target).value;
    this.tableActions();
  }

  orderBy() {
    this.ascending = !this.ascending;
    this.tableActions();
  }

  tableActions() {
    if (this.typeFilter != '') {
      this.http.get<WorkItem[]>('api/WorkItem/SortByDateAndFilterByType?type=' + this.typeFilter + '&ascending=' + this.ascending).subscribe(result => {
        this.workItems = result;
      }, error => console.error(error));
    }
    else {
      this.http.get<WorkItem[]>('api/WorkItem/GetAllSortByDate?ascending=' + this.ascending).subscribe(result => {
        this.workItems = result;
      }, error => console.error(error));
    }
  }
}

interface WorkItem {
  id: number;
  type: string;
  title: string;
  createdOn: string;
}
