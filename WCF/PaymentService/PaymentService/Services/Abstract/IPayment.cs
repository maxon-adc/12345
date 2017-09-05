﻿using PaymentService.Dtos;
using System;
using System.ServiceModel;

namespace PaymentService.Services.Abstract
{
	[ServiceContract]
	public interface IPayment
	{
		[OperationContract]
		TransactionResponse ConductPurchase(Transaction transaction);

		[OperationContract]
		TransactionResponse ConfirmTransaction(Guid transactionId, int confirmationCode);
	}
}