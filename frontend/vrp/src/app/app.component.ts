import { Component, OnInit } from '@angular/core';
import { Loader } from '@googlemaps/js-api-loader';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'vrp';
  map: any;
  markers: any[] = [];
  marker: any;
  polygon: any;

  initMap(): any {
    let loader = new Loader({
      apiKey: 'AIzaSyD3GG7Qq1XgRMAcjPejT9spgnR4RZ9xzbU',
    });

    const position = { lat: 23.8103, lng: 90.4125 };

    loader.load().then(async () => {
      const { Map } = (await google.maps.importLibrary(
        'maps'
      )) as google.maps.MapsLibrary;

      this.map = new Map(document.getElementById('map') as HTMLElement, {
        center: position,
        zoom: 7,
      });

      // Add event listener
      this.map.addListener('click', (event: any) => {
        console.log(
          `Map Clicked at (${event.latLng.lat()}, ${event.latLng.lng()})`
        );
        this.handleMapClick(event);
      });
    });
  }

  handleMapClick(event: any): void {
    const lat = event.latLng.lat();
    const lng = event.latLng.lng();

    this.markers.push({ lat, lng });
    this.setMarker(lat, lng);

    // this.setPolygon();

    console.log(this.markers);
  }

  setMarker(lat: any, lng: any) {
    this.marker = new google.maps.Marker({
      position: { lat, lng },
      map: this.map,
      label: '1',
    });

    google.maps.event.addListener(this.marker, 'click', (event: any) => {
      console.log(
        `Marker Clicked at (${event.latLng.lat()}, ${event.latLng.lng()})`
      );
      // this.removeMarker(event.latLng.lat(), event.latLng.lng());
    });
  }

  resetAllMarker() {
    this.marker.setMap(null);
  }

  removeMarker(lat: any, lng: any) {
    // Remove the marker from the map
    this.marker.setMap(null);

    // Remove the marker from the array
    let index = -1;
    for (let i = 0; i < this.markers.length; i++) {
      if (this.markers[i].lat == lat && this.markers[i].lng == lng) {
        index = i;
        break;
      }
    }
    console.log(`${lat} ${lng} - ${index}`);
     this.markers.splice(index, 1);
    for (let i = 0; i < this.markers.length; i++) {
      this.setMarker(this.markers[i].lat, this.markers[i].lng)
    }
    this.setPolygon();
  }

  setPolygon() {
    //reset old polygon
    if (this.polygon) {
      this.polygon.setMap(null); // Clear previous polygon
    }

    // Create an array of marker coordinates
    const polygonCoords = this.markers;

    // Create a polygon
    this.polygon = new google.maps.Polygon({
      paths: polygonCoords,
      editable: true, // Allow user to edit the polygon
      draggable: true, // Allow user to drag the polygon
    });

    // Set the polygon on the map
    this.polygon.setMap(this.map);

    console.log('Current markers: ' + polygonCoords);
  }

  ngOnInit(): void {
    this.initMap();
  }
}
