using System;
using System.Text;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using Din91379;

namespace Din91379Benchmarks
{
    public class RegexVsExplicit
    {
        private const string Value = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþÿĀāĂăĄąĆćĈĉĊċČčĎďĐđĒēĔĕĖėĘęĚěĜĝĞğĠġĢģĤĥĦħĨĩĪīĬĭĮįİıĲĳĴĵĶķĸĹĺĻļĽľĿŀŁłŃńŅņŇňŉŊŋŌōŎŏŐőŒœŔŕŖŗŘřŚśŜŝŞşŠšŢţŤťŦŧŨũŪūŬŭŮůŰűŲųŴŵŶŷŸŹźŻżŽžƇƈƏƗƠơƯưƷǍǎǏǐǑǒǓǔǕǖǗǘǙǚǛǜǞǟǢǣǤǥǦǧǨǩǪǫǬǭǮǯǰǴǵǸǹǺǻǼǽǾǿȒȓȘșȚțȞȟȧȨȩȪȫȬȭȮȯȰȱȲȳəɨʒḂḃḆḇḊḋḌḍḎḏḐḑḜḝḞḟḠḡḢḣḤḥḦḧḨḩḪḫḯḰḱḲḳḴḵḶḷḺḻṀṁṂṃṄṅṆṇṈṉṒṓṔṕṖṗṘṙṚṛṞṟṠṡṢṣṪṫṬṭṮṯẀẁẂẃẄẅẆẇẌẍẎẏẐẑẒẓẔẕẖẗẞẠạẢảẤấẦầẨẩẪẫẬậẮắẰằẲẳẴẵẶặẸẹẺẻẼẽẾếỀềỂểỄễỆệỈỉỊịỌọỎỏỐốỒồỔổỖỗỘộỚớỜờỞởỠỡỢợỤụỦủỨứỪừỬửỮữỰựỲỳỴỵỶỷỸỹA̋C̀C̄C̆C̈C̕C̣C̦C̨̆D̂F̀F̄G̀H̄H̦H̱J́J̌K̀K̂K̄K̇K̕K̛K̦K͟HK͟hL̂L̥L̥̄L̦M̀M̂M̆M̐N̂N̄N̆N̦P̀P̄P̕P̣R̆R̥R̥̄S̀S̄S̛̄S̱T̀T̄T̈T̕T̛U̇Z̀Z̄Z̆Z̈Z̧a̋c̀c̄c̆c̈c̕c̣c̦c̨̆d̂f̀f̄g̀h̄h̦j́k̀k̂k̄k̇k̕k̛k̦k͟hl̂l̥l̥̄l̦m̀m̂m̆m̐n̂n̄n̆n̦p̀p̄p̕p̣r̆r̥r̥̄s̀s̄s̛̄s̱t̀t̄t̕t̛u̇z̀z̄z̆z̈z̧Ç̆Û̄ç̆û̄ÿ́Č̕Č̣č̕č̣Ī́ī́Ž̦Ž̧ž̦ž̧Ḳ̄ḳ̄Ṣ̄ṣ̄Ṭ̄ṭ̄Ạ̈ạ̈Ọ̈ọ̈Ụ̄Ụ̈ụ̄ụ̈ē̍ḗō̍";

        // Pattern copied from din-91379-datatypes.xsd
        private static readonly Regex Pattern = new Regex(
            "^([\u0009-\u000A]|\u000D|[\u0020-\u007E]|[\u00A0-\u00AC]|[\u00AE-\u017E]|[\u0187-\u0188]|\u018F|\u0192|\u0197|[\u01A0-\u01A1]|[\u01AF-\u01B0]|\u01B7|[\u01CD-\u01DC]|[\u01DE-\u01DF]|[\u01E2-\u01F0]|[\u01F4-\u01F5]|[\u01F8-\u01FF]|[\u0212-\u0213]|[\u0218-\u021B]|[\u021E-\u021F]|[\u0227-\u0233]|\u0259|\u0268|\u0292|\u02B0|\u02B3|[\u02B9-\u02BA]|[\u02BE-\u02BF]|\u02C6|\u02C8|\u02CC|\u02DC|\u02E2|\u0386|[\u0388-\u038A]|\u038C|[\u038E-\u03A1]|[\u03A3-\u03CE]|\u040D|[\u0410-\u042A]|\u042C|[\u042E-\u044A]|\u044C|[\u044E-\u044F]|\u045D|\u1D48|\u1D57|[\u1E02-\u1E03]|[\u1E06-\u1E07]|[\u1E0A-\u1E11]|[\u1E1C-\u1E2B]|[\u1E2F-\u1E37]|[\u1E3A-\u1E3B]|[\u1E40-\u1E49]|[\u1E52-\u1E5B]|[\u1E5E-\u1E63]|[\u1E6A-\u1E6F]|[\u1E80-\u1E87]|[\u1E8C-\u1E97]|\u1E9E|[\u1EA0-\u1EF9]|[\u2018-\u201A]|[\u201C-\u201E]|[\u2020-\u2021]|\u2026|\u2030|[\u2039-\u203A]|\u2070|[\u2074-\u2079]|[\u207F-\u2089]|\u20AC|\u2122|\u221E|[\u2264-\u2265]|\u0041\u030B|\u0043(\u0300|\u0304|\u0306|\u0308|\u0315|\u0323|\u0326|\u0328\u0306)|\u0044\u0302|\u0046(\u0300|\u0304)|\u0047\u0300|\u0048(\u0304|\u0326|\u0331)|\u004A(\u0301|\u030C)|\u004B(\u0300|\u0302|\u0304|\u0307|\u0315|\u031B|\u0326|\u035F\u0048|\u035F\u0068)|\u004C(\u0302|\u0325|\u0325\u0304|\u0326)|\u004D(\u0300|\u0302|\u0306|\u0310)|\u004E(\u0302|\u0304|\u0306|\u0326)|\u0050(\u0300|\u0304|\u0315|\u0323)|\u0052(\u0306|\u0325|\u0325\u0304)|\u0053(\u0300|\u0304|\u031B\u0304|\u0331)|\u0054(\u0300|\u0304|\u0308|\u0315|\u031B)|\u0055\u0307|\u005A(\u0300|\u0304|\u0306|\u0308|\u0327)|\u0061\u030B|\u0063(\u0300|\u0304|\u0306|\u0308|\u0315|\u0323|\u0326|\u0328\u0306)|\u0064\u0302|\u0066(\u0300|\u0304)|\u0067\u0300|\u0068(\u0304|\u0326)|\u006A\u0301|\u006B(\u0300|\u0302|\u0304|\u0307|\u0315|\u031B|\u0326|\u035F\u0068)|\u006C(\u0302|\u0325|\u0325\u0304|\u0326)|\u006D(\u0300|\u0302|\u0306|\u0310)|\u006E(\u0302|\u0304|\u0306|\u0326)|\u0070(\u0300|\u0304|\u0315|\u0323)|\u0072(\u0306|\u0325|\u0325\u0304)|\u0073(\u0300|\u0304|\u031B\u0304|\u0331)|\u0074(\u0300|\u0304|\u0315|\u031B)|\u0075\u0307|\u007A(\u0300|\u0304|\u0306|\u0308|\u0327)|\u00C7\u0306|\u00DB\u0304|\u00E7\u0306|\u00FB\u0304|\u00FF\u0301|\u010C(\u0315|\u0323)|\u010D(\u0315|\u0323)|\u012A\u0301|\u012B\u0301|\u017D(\u0326|\u0327)|\u017E(\u0326|\u0327)|\u1E32\u0304|\u1E33\u0304|\u1E62\u0304|\u1E63\u0304|\u1E6C\u0304|\u1E6D\u0304|\u1EA0\u0308|\u1EA1\u0308|\u1ECC\u0308|\u1ECD\u0308|\u1EE4(\u0304|\u0308)|\u1EE5(\u0304|\u0308))*$",
            RegexOptions.Compiled
        );

        [Benchmark(Baseline = true)]
        public bool Explicit()
        {
            return TypeE.IsValid(Value);
        }

        [Benchmark]
        public bool regex()
        {
            return Pattern.IsMatch(Value);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<RegexVsExplicit>();
        }
    }
}