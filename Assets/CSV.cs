using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine;

public class CSV : MonoBehaviour
{
    public static CSV instance;

    private void Awake()
    {
        instance = this;
    }

    public List<UserInfo> Parse(string fileName)
    {
        // 전체 UserInfo 를 가지고 있을 List
        List<UserInfo> list = new List<UserInfo>();


        //file을 읽어오자
        string path = Application.streamingAssetsPath + "/" + fileName + ".csv";
        string stringData = File.ReadAllText(path);
        
        //"\n" or "\r\n"
        //엔터를 기준으로 한줄 한줄 자르자

        //"\n"으로 자르자.
        string [] lines = stringData.Split("\n");
        
        for(int i = 0; i < lines.Length; i++)
        {
            //남아있는 "\r" 을 기준으로 놔누자.
            string[] temp = lines[i].Split("\r");
            lines[i] = temp[0];
        }

        // , 를 기준으로 변수를 나누자.
        string[] variables = lines[0].Split(",");

        // , 를 기준으로 값을 나누자.
        for(int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(",");

            // 잘라진 데이터를 가지고 UserInfo에 셋팅해서 리스트에 추가.
            UserInfo info = new UserInfo();
            info.name = values[0];
            info.phone = values[1];
            info.email = values[2];
            info.age = int.Parse(values[3]);
            info.gender = bool.Parse(values[4]);

            // list 에 채우자
            list.Add(info);
        }

        return list;
    }

    public List<T> Parse<T>(string fileName) where T : new()
    {
        // 전체 T 를 가지고 있을 List
        List<T> list = new List<T>();

        //file을 읽어오자
        string path = Application.streamingAssetsPath + "/" + fileName + ".csv";
        string stringData = File.ReadAllText(path);
        //"\n" or "\r\n"
        //엔터를 기준으로 한줄 한줄 자르자

        //"\n"으로 자르자.
        string[] lines = stringData.Split("\n");

        for (int i = 0; i < lines.Length; i++)
        {
            //남아있는 "\r" 을 기준으로 놔누자.
            string[] temp = lines[i].Split("\r");
            lines[i] = temp[0];
        }

        // , 를 기준으로 변수를 나누자.
        string[] variables = lines[0].Split(",");

        // , 를 기준으로 값을 나누자.
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(",");

            // 잘라진 데이터를 가지고 UserInfo에 셋팅해서 리스트에 추가.
            T info = new T();

            for(int j = 0; j < variables.Length; j++)
            {
                //T 에있는 변수들의 정보를 가져오자.
                System.Reflection.FieldInfo fieldInfo = typeof(T).GetField(variables[j]);
                //int.parse, byte.parse, bool.parse 것들을 fieldInfo 를 이용해서 하자
                TypeConverter typeConverter = TypeDescriptor.GetConverter(fieldInfo.FieldType);
                //values 를 typeConverter 를 이용해서 변수에 셋팅
                if (values[j].Length > 0)
                {
                    fieldInfo.SetValue(info, typeConverter.ConvertFrom(values[j]));
                }
            }
            
            // list 에 채우자
            list.Add(info);
        }

        return list;
    }
}
