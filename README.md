
# DSS (Decision Support System) for Kingsford Waterbay Residents

This DSS (Decision Support System) is an aid to help residents of Kingsford Waterbay, Upper Serangoon View, S5883884 to decide which transport to take (Shuttle vs Public Bus) based on shuttle schedule and bus timings arrival.






## API References

#### Get the latest Kingsford Waterbay Shuttle service timing based on latest timetable schedule

```http
  GET timing/getClosestDepartureByRoute?route={route}
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `route` | `string` | **Required**. Incoming or Outgoing of Kingsford Waterbay |

#### Get the latest departures at a given bus stop code, data fetched from LTA DataMall

```http
  GET timing/getBusArrivalsByBusStopCode?busStopCode={busStopCode}&serviceNo={serviceNo}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `busStopCode`  | `string` | **Required**. Bus stop code |
| `serviceNo`    | `string` | **Optional**. Specific Bus Service No |

#### Get the transport decision (to and from Kingsford Waterbay)

```http
  GET timing/getKwbDecision?route={route}
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `route` | `string` | **Required**. Incoming or Outgoing of Kingsford Waterbay |



## Demo

- [Web based] https://eleazlawn.azurewebsites.net
- [API usage] via WidgetWorks iOS App
  ![ScreenRecording_11-05-202417-57-39_1-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/c12905aa-a1de-49e9-b292-307f46259bb9)




## Acknowledgements

 - [LTA SG Data Mall](https://datamall.lta.gov.sg/content/datamall/en.html)

