// Copyright 2024 Google Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// [START spanner_create_incremental_backup_schedule]
using Google.Cloud.Spanner.Admin.Database.V1;
using Google.Cloud.Spanner.Common.V1;
using Google.Protobuf.WellKnownTypes;
using System;

public class CreateIncrementalBackupScheduleSample
{
    public BackupSchedule CreateIncrementalBackupSchedule(string projectId, string instanceId, string databaseId, string scheduleId)
    {
        DatabaseAdminClient client = DatabaseAdminClient.Create();

        BackupSchedule response = client.CreateBackupSchedule(
            new CreateBackupScheduleRequest
            {
                ParentAsDatabaseName = DatabaseName.FromProjectInstanceDatabase(projectId, instanceId, databaseId),
                BackupScheduleId = scheduleId,
                BackupSchedule = new BackupSchedule
                {
                    Spec = new BackupScheduleSpec
                    {
                        CronSpec = new CrontabSpec
                        {
                            Text = "30 12 * * *",
                        }
                    },
                    RetentionDuration = new Duration
                    {
                        Seconds = 86400,
                    },
                    EncryptionConfig = new CreateBackupEncryptionConfig
                    {
                        EncryptionType = CreateBackupEncryptionConfig.Types.EncryptionType.GoogleDefaultEncryption,
                    },
                    IncrementalBackupSpec = new IncrementalBackupSpec { },
                }
            });

        Console.WriteLine($"Created incremental backup schedule: {response}");
        return response;
    }
}
// [END spanner_create_incremental_backup_schedule]
