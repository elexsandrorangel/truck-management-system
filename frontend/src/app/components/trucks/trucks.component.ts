import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {TruckService} from "../../services/truck.service";
import Swal from 'sweetalert2';

@Component({
  selector: 'app-trucks',
  templateUrl: './trucks.component.html',
  styleUrls: ['./trucks.component.css']
})
export class TrucksComponent implements OnInit, AfterViewInit {
  trucks: any[] = [];

  displayedColumnsMap: any[] = [
    {title: 'Model', field: 'model'},
    {title: 'Color', field: 'color'},
    {title: 'Manufacture Year ', field: 'manufactureYear'},
    {title: 'Model Year', field: 'modelYear'},
    {title: 'action', field: ''}
  ];

  trucksDataSource = new MatTableDataSource();
  displayedColumnsNames: string[] = this.displayedColumnsMap.map(x => x.title);
  displayedColumnsWithValues: any[] = this.displayedColumnsMap.filter(x => x.field);

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private truckService: TruckService) { }

  ngOnInit(): void {
    this.renderTrucks();
  }

  ngAfterViewInit(): void {
    this.trucksDataSource.paginator = this.paginator;
    this.trucksDataSource.sort = this.sort;
  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.trucksDataSource.filter = filterValue.trim().toLowerCase();

    if (this.trucksDataSource.paginator) {
      this.trucksDataSource.paginator.firstPage();
    }
  }

  sortData($event: any) : void {
    const sortId = $event.active;
    const sortDir = $event.sortDirection;
    if ('asc' === sortDir){
      this.trucksDataSource.data = this.trucks.slice().sort(
        (a, b) => a[sortId] > b[sortId] ? -1 : a[sortId] < b[sortId] ? 1 : 0
      );
    } else {
      this.trucksDataSource.data = this.trucks.slice().sort(
        (a, b) => a[sortId] < b[sortId] ? -1 : a[sortId] > b[sortId] ? 1 : 0
      );
    }
  }

  renderTrucks() : void {
    this.truckService.getAll().subscribe(l => {
      this.trucks = l;
      this.trucksDataSource.data = l;
    }, (error: any) => {
      console.log(error);
    });
  }

  deleteTruck(truck: any): void {
    this.truckService.delete(truck.id).subscribe((res: any) => {
      this.renderTrucks();
      Swal.fire('Deleted', 'Truck deleted successfully', 'success');
    }, (error: any) => {
      console.log(error);
      Swal.fire('Error', 'An error happened during operation', 'error');
    });
  }

  openDialog(action: string, data: any) {
    if (action.toLowerCase() === 'delete'){
      this.handleDeleteAlert(data);
    }
  }

  handleDeleteAlert(data: any): void {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this truck entry!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it',
    }).then((result: any) => {
      if (result.isConfirmed) {
        this.deleteTruck(data);
      }
    });
  }

}
