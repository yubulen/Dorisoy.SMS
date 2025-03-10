﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Data
{
  /// <summary>
  /// 用于表示系统日志
  /// </summary>
   public class NLog : BaseEntity
  {
        public string MachineName { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Logged { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
        public string Properties { get; set; }
        public string Callsite { get; set; }
        public string Exception { get; set; }

        [MaxLength(50)]
        public string Source { get; set; }
    }
}
