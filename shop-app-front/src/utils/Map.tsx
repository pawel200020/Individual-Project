import {MapContainer, Marker, TileLayer, useMapEvent} from "react-leaflet";
import L from  'leaflet'
import icon from 'leaflet/dist/images/marker-icon.png'
import iconShadow from 'leaflet/dist/images/marker-shadow.png'
import 'leaflet/dist/leaflet.css'
import coordinatesDTO from "./coordinates.model";
import {useState} from "react";

let defaultIcon = L.icon({
    iconUrl: icon,
    shadowUrl:iconShadow,
    iconAnchor: [16,37]
    }
);
L.Marker.prototype.options.icon=defaultIcon;
export default function Map(props: MapProps){
    const [coordinates,setCoordinates] = useState<coordinatesDTO[]>([])
    return(
        <>
            <MapContainer center={[50.029871, 19.906259]} zoom={14} style={{height: props.height}}>
                <TileLayer attribution="Shop" url='https://tile.openstreetmap.org/{z}/{x}/{y}.png'/>
            <MapClick setCoordinates={coordinates => {
                setCoordinates([coordinates])
            }}/>
                {coordinates.map((coordinate, index)=> <Marker key={index} position= {[coordinate.lat,coordinate.lng]}/>)}
            </MapContainer>
        </>
    )
}
function MapClick(props: mapClickProps){
    useMapEvent('click', eventArgs=>{
        props.setCoordinates({lat:eventArgs.latlng.lat,lng: eventArgs.latlng.lng})
    });
    return null
}
interface mapClickProps{
    setCoordinates( coordinates: coordinatesDTO): void;
}

interface MapProps{
    height: string;
}
Map.defaultProps = {
    height: '500px'
}