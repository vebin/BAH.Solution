﻿using Kingdee.BOS.Core.Interaction;
using Kingdee.BOS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS.Core.DynamicForm
{
    public static class IOperationResultExtension
    {
        public static string GetErrorMessage(this IOperationResult result)
        {
            StringBuilder msg = new StringBuilder();
            result.ValidationErrors.ForEach(error => msg.AppendLine(string.Format("主键：{0}，错误：{1}", error.BillPKID, error.Message)));
            result.GetFatalErrorResults().ForEach(error => msg.AppendLine(string.Format("主键：{0}，错误：{1}", error.BillPKID, error.Message)));
            return msg.ToString();
        }//end static method

        public static string GetErrorMessage(this IOperationResult result, string description)
        {
            StringBuilder msg = new StringBuilder();
            result.ValidationErrors.ForEach(error => msg.AppendLine(string.Format("[{0}]主键：{1}，错误：{2}", description, error.BillPKID, error.Message)));
            result.GetFatalErrorResults().ForEach(error => msg.AppendLine(string.Format("[{0}]主键：{1}，错误：{2}", description, error.BillPKID, error.Message)));
            return msg.ToString();
        }//end static method

        public static string GetResultMessage(this IOperationResult result)
        {
            result.MergeValidateErrors();
            StringBuilder msg = new StringBuilder();
            if (result.InteractionContext != null && result.InteractionContext.Option.GetInteractionFlag().Any())
            {
                msg.AppendLine("因交互性提示而操作中断！");
            }
            /*
            foreach (var error in result.ValidationErrors)
            {
                msg.AppendLine(error.Message);
            }
            foreach (var error in result.GetFatalErrorResults())
            {
                msg.AppendLine(error.Message);
            }
            */
            foreach (var operate in result.OperateResult)
            {
                if (!operate.Message.IsNullOrEmptyOrWhiteSpace()) msg.AppendLine(operate.Message);
            }
            return msg.ToString();
        }//end static method

        public static void ThrowWhenUnSuccess(this IOperationResult result, Func<IOperationResult, string> predicate)
        {
            if (result.IsSuccess) return;

            string message = predicate(result);
            throw new KDBusinessException(string.Empty, message);
        }//end static method

        public static void ThrowWhenUnSuccess(this IOperationResult result, IOperationResult parent, Func<IOperationResult, string> predicate)
        {
            if (result.IsSuccess) return;
            if (result.InteractionContext != null && result.InteractionContext.Option.GetInteractionFlag().Any())
            {
                parent.InteractionContext = result.InteractionContext;
                parent.Sponsor = result.Sponsor;
            }//end if

            ThrowWhenUnSuccess(result, predicate);
        }//end static method

        public static void ThrowWhenUnSuccess(this IOperationResult result)
        {
            ThrowWhenUnSuccess(result, op => op.GetResultMessage());
        }//end static method

        public static IOperationResult RepairPKValue(this IOperationResult result)
        {
            result.OperateResult
                  .Where(item => item.PKValueIsNullOrEmpty)
                  .Join(result.MapSuccessDataEnityIndex, left => left.DataEntityIndex, right => right.Value, (left, right) =>
                  {
                      left.PKValue = right.Key;
                      return left;
                  }).ToArray();
            return result;
        }//end static method

    }//end static class
}//end namespace
