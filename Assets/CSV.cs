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
        // ��ü UserInfo �� ������ ���� List
        List<UserInfo> list = new List<UserInfo>();


        //file�� �о����
        string path = Application.streamingAssetsPath + "/" + fileName + ".csv";
        string stringData = File.ReadAllText(path);
        
        //"\n" or "\r\n"
        //���͸� �������� ���� ���� �ڸ���

        //"\n"���� �ڸ���.
        string [] lines = stringData.Split("\n");
        
        for(int i = 0; i < lines.Length; i++)
        {
            //�����ִ� "\r" �� �������� ������.
            string[] temp = lines[i].Split("\r");
            lines[i] = temp[0];
        }

        // , �� �������� ������ ������.
        string[] variables = lines[0].Split(",");

        // , �� �������� ���� ������.
        for(int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(",");

            // �߶��� �����͸� ������ UserInfo�� �����ؼ� ����Ʈ�� �߰�.
            UserInfo info = new UserInfo();
            info.name = values[0];
            info.phone = values[1];
            info.email = values[2];
            info.age = int.Parse(values[3]);
            info.gender = bool.Parse(values[4]);

            // list �� ä����
            list.Add(info);
        }

        return list;
    }

    public List<T> Parse<T>(string fileName) where T : new()
    {
        // ��ü T �� ������ ���� List
        List<T> list = new List<T>();

        //file�� �о����
        string path = Application.streamingAssetsPath + "/" + fileName + ".csv";
        string stringData = File.ReadAllText(path);
        //"\n" or "\r\n"
        //���͸� �������� ���� ���� �ڸ���

        //"\n"���� �ڸ���.
        string[] lines = stringData.Split("\n");

        for (int i = 0; i < lines.Length; i++)
        {
            //�����ִ� "\r" �� �������� ������.
            string[] temp = lines[i].Split("\r");
            lines[i] = temp[0];
        }

        // , �� �������� ������ ������.
        string[] variables = lines[0].Split(",");

        // , �� �������� ���� ������.
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(",");

            // �߶��� �����͸� ������ UserInfo�� �����ؼ� ����Ʈ�� �߰�.
            T info = new T();

            for(int j = 0; j < variables.Length; j++)
            {
                //T ���ִ� �������� ������ ��������.
                System.Reflection.FieldInfo fieldInfo = typeof(T).GetField(variables[j]);
                //int.parse, byte.parse, bool.parse �͵��� fieldInfo �� �̿��ؼ� ����
                TypeConverter typeConverter = TypeDescriptor.GetConverter(fieldInfo.FieldType);
                //values �� typeConverter �� �̿��ؼ� ������ ����
                if (values[j].Length > 0)
                {
                    fieldInfo.SetValue(info, typeConverter.ConvertFrom(values[j]));
                }
            }
            
            // list �� ä����
            list.Add(info);
        }

        return list;
    }
}
