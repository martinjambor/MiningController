namespace Desktop
{
    public class PoolItem
    {
        public string pool_address { get; set; }
        public string wallet_address { get; set; }
        public string rig_id { get; set; }
        public string pool_password { get; set; }
        public bool use_nicehash { get; set; }
        public bool use_tls { get; set; }
        public string tls_fingerprint { get; set; }
        public int pool_weight { get; set; }
    }
}