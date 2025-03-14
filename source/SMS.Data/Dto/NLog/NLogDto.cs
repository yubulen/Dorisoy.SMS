﻿using System;

namespace SMS.Data.Dto
{
    /// <summary>
    /// 数据对象映射
    /// </summary>
    public class NLogDto
    {
        public Guid Id { get; set; }
        public string MachineName { get; set; }
        public DateTime Logged { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
        public string Properties { get; set; }
        public string Callsite { get; set; }
        public string Exception { get; set; }
        public string Source { get; set; }
    }
}
