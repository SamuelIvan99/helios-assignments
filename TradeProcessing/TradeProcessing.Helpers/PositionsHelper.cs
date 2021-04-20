using System;
using System.Threading.Tasks;
using TradeProcessing.DataAceess;

namespace TradeProcessing.Helpers
{
    public class PositionsHelper
    {
        readonly HeliosContext _context;
        readonly string _positionsKey;

        public PositionsHelper(HeliosContext context, string positionsKey)
        {
            _context = context;
            _positionsKey = positionsKey;
        }

        public async Task<long> GetCurrentPosition()
        {
            var positionSettings = await _context.Settings.FindAsync(_positionsKey);

            return positionSettings?.LongValue ?? -1;
        }

        public async Task<String> GetCurrentString()
        {
            var positionSettings = await _context.Settings.FindAsync(_positionsKey);

            return positionSettings?.StringValue ?? String.Empty;
        }

        public async Task SetPosition(long value)
        {
            var existing = await _context.Settings.FindAsync(_positionsKey);

            if (existing == null)
            {
                await _context.Settings.AddAsync(new Setting(id: _positionsKey, longValue: value));
                return;
            }

            existing.SetLongValue(value);
        }

        public async Task SetStringValue(string value)
        {
            var existing = await _context.Settings.FindAsync(_positionsKey);

            if (existing == null)
            {
                await _context.Settings.AddAsync(new Setting(id: _positionsKey, stringValue: value));
                return;
            }

            existing.SetStringValue(value);
        }
    }

    public static class PositionsHelperContextExtensions
    {
        public static PositionsHelper GetPositionsHelper(this HeliosContext context, string positionsKey) => new PositionsHelper(context, positionsKey);
    }
}