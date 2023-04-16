using ASPNetMVC.Abstraction.Model.Entities;
using Dapper;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Repositories;
using SHUNetMVC.Infrastructure.EntityFramework.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Repositories
{
    public class TXAttachmentRepository : BaseCrudRepository<TX_Attachment, TXAttachmentDto, TXAttachmentDto, TXAttachmentQuery>, ITXAttachmentRepository
    {
        public TXAttachmentRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
        : base(explorationContext, connection, new TXAttachmentQuery(), hrContext)
        {

        }

        public override async Task Create(TXAttachmentDto model)
        {
            var entity = model.ToEntity();
            try
            {
                _explorationContext.TX_Attachment.Add(entity);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public override async Task<LookupList> GetAdaptiveFilterList(string columnId, string usernameSession)
        {
            var result = new LookupList
            {
                ColumnId = columnId
            };

            using (var connection = OpenConnection())
            {
                var items = await connection.QueryAsync<string>($"SELECT DISTINCT {columnId} FROM dbo.TX_Attachment ORDER BY {columnId}");

                result.Items = items.Select(item => new LookupItem
                {
                    Text = item,
                    Value = item
                }).ToList();
            }


            result.Items = result.Items.GroupBy(o => o.Text).Select(o => o.FirstOrDefault()).ToList();

            return result;
        }

        public async Task<List<TXAttachmentDto>> GetAttachmentByStructureID(string structureID)
        {
            var getResult = await GetLookupListText(structureID);
            return getResult.ToList();
        }

        public async Task<TXAttachmentDto> GetOneByTwoParameter(string TransactionID, string FileCategoryParID)
        {
            try
            {
                var entity = await _explorationContext.TX_Attachment.FindAsync(new object[] { TransactionID, FileCategoryParID });
                var dto = new TXAttachmentDto(entity);
                return dto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task Destroy(string TransactionID, int FileID)
        {
            try
            {
                var item = await _explorationContext.Set<TX_Attachment>().FindAsync(new object[] { TransactionID, FileID });
                _explorationContext.Set<TX_Attachment>().Remove(item);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> GetParamListIDByTwoParam(string paramID, string paramValueText)
        {
            try
            {
                var getParamListID = await GetLookupListTextWithParam(paramID, paramValueText);
                return getParamListID;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int GenerateNewID()
        {
            try
            {
                Int32 unixTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                return unixTimestamp;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
