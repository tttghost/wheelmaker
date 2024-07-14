using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;
using Cargold.DB.TableImporter;

namespace Cargold
{
    public partial class LibraryRemocon
    {
        public partial class FrameWorkData
        {
            public partial class DatabaseData
            {
                public partial class TableImporterData
                {
#if UNITY_EDITOR
                    [FoldoutGroup(Editor_C.Optional), Button("딕셔너리 직렬화 재정의")]
                    private void CallEdit_Override_Func()
                    {
                        LibraryRemocon.Instance.frameWorkData.databaseData.tableImporterData.GetLibrary = new OverrideClass();

                        Debug_C.Log_Func("테이블 임포터의 딕셔너리 직렬화가 재정의되었습니다.");

                        Editor_C.SetSaveAsset_Func(LibraryRemocon.Instance);
                    }
#endif

                    [System.Serializable]
                    public class OverrideClass : Cargold.LibraryRemocon.FrameWorkData.DatabaseData.TableImporterData.ILibrary
                    {
                        [ShowInInspector, LabelText("재정의 여부")] public bool IsOverride => true;

                        public string GetSerialize_Func(Dictionary<string, SheetData> _dic)
                        {
                            // return Newtonsoft.Json.JsonConvert.SerializeObject(_dic);

                            return default;
                        }
                        public Dictionary<string, DB.TableImporter.SheetData> GetDerialize_Func(string _jsonStr)
                        {
                            // return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, SheetData>>(_jsonStr);

                            return default;
                        }
                    }
                }
            }
        }
    }
}
