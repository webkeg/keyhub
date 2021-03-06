﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using KeyHub.Data;
using KeyHub.Model;

namespace KeyHub.Web.ViewModels.Transaction
{
    /// <summary>
    /// Viewmodel for index list of a Transaction
    /// </summary>
    public class TransactionIndexViewModel : RedirectUrlModel
    {
        public TransactionIndexViewModel() : base() { }

        /// <summary>
        /// Construct the viewmodel
        /// </summary>
        /// <param name="transactionList">List of transaction entities</param>
        public TransactionIndexViewModel(List<Model.Transaction> transactionList):this()
        {
            Transactions = new List<TransactionIndexViewItem>();
            foreach (Model.Transaction entity in transactionList)
            {
                Transactions.Add(new TransactionIndexViewItem(entity, entity.TransactionItems));
            }
        }

        /// <summary>
        /// List of transactions
        /// </summary>
        public List<TransactionIndexViewItem> Transactions { get; set; }
    }

    /// <summary>
    /// TransactionViewModel extension that includes the name of the countrycode assinged country
    /// </summary>
    public class TransactionIndexViewItem : TransactionViewModel
    {
        public TransactionIndexViewItem(Model.Transaction transaction, IEnumerable<Model.TransactionItem> transactionItems)
            : base(transaction)
        {
            PurchaserName = (transactionItems.FirstOrDefault().License != null) ?
                transactionItems.FirstOrDefault().License.PurchasingCustomer.Name : transaction.PurchaserName;

            SKUSummary =  transactionItems.ToSummary(x => (x.Sku != null) ? x.Sku.SkuCode : "None", 3, ", ");

            StatusName = transaction.Status.GetDescription<TransactionStatus>();

            IsWaitingForClaim = transaction.IsWaitingForClaim;
        }

        /// <summary>
        /// Name of the purchasing customer.
        /// </summary>
        [DisplayName("Purchased by")]
        public string PurchaserName { get; set; }

        /// <summary>
        /// SKU summary.
        /// </summary>
        [DisplayName("SKUs")]
        public string SKUSummary { get; set; }

        /// <summary>
        /// Status of the transaction
        /// </summary>
        [DisplayName("Status")]
        public string StatusName { get; set; }

        /// <summary>
        /// Indicate if tranaction status is waiting for claim
        /// </summary>
        [DisplayName("")]
        public bool IsWaitingForClaim { get; set; }
    }
}