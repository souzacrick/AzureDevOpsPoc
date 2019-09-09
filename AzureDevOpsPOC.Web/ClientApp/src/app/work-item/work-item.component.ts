import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-work-item',
  templateUrl: './work-item.component.html'
})

export class WorkItemComponent {
  public workItems: WorkItem[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<WorkItem[]>(baseUrl + 'api/WorkItem/GetAll').subscribe(result => {
      this.workItems = result;
    }, error => console.error(error));
  }
}

interface WorkItem {
  id: number;
  type: string;
  title: string;
  createdOn: string;
}
