﻿namespace BikeRental.Rental

open System
open BikeRental

type BookingId = BookingId of Guid

type BookingEventData =
    | Booked of UserId
    | Released

type BookingEvent =
    {
        BookingId: BookingId
        EventId: Guid
        BikeId: BikeId
        Data: BookingEventData
        Instant: Instant
    }
