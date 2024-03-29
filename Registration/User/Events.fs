﻿namespace BikeRental.Registration

open System
open BikeRental

type Username = Username of string
type PhoneNumber = PhoneNumber of string
type PasswordHash = PasswordHash of string
type RegistrationCompletionId = RegistrationCompletionId of Guid

type RegistrationStartedData =
    { PhoneNumber: PhoneNumber
      CompletionId: RegistrationCompletionId }

type RegistrationCompletedData =
    { PasswordHash: PasswordHash
      FirstName: string
      LastName: string }

type UserEventData =
    | RegistrationStarted of RegistrationStartedData
    | RegistrationCompleted of RegistrationCompletedData
    | UserDeactivated

type UserEvent =
    { UserId: UserId
      EventId : Guid
      Username: Username
      Data: UserEventData
      Instant: Instant }
